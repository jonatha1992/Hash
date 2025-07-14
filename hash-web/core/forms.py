from django import forms
from .models import FormularioHash, Oficial, TipoProcedimiento, FormularioCustodia, PersonalCustodia, Jerarquia, Destino

class FormularioHashForm(forms.ModelForm):
    class Meta:
        model = FormularioHash
        fields = [
            'nro_hash',
            'tipo',
            'procedimiento',
            'tipo_procedimiento',
            'oficial_entrega',
            'oficial_recibe',
            'observaciones',
        ]
        widgets = {
            'nro_hash': forms.NumberInput(attrs={'class': 'form-control'}),
            'tipo': forms.Select(attrs={'class': 'form-select'}),
            'procedimiento': forms.TextInput(attrs={'class': 'form-control'}),
            'tipo_procedimiento': forms.Select(attrs={'class': 'form-select'}),
            'oficial_entrega': forms.Select(attrs={'class': 'form-select'}),
            'oficial_recibe': forms.Select(attrs={'class': 'form-select'}),
            'observaciones': forms.Textarea(attrs={'class': 'form-control', 'rows': 3}),
        }

    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        self.fields['oficial_entrega'].queryset = Oficial.objects.filter(activo=True)
        self.fields['oficial_recibe'].queryset = Oficial.objects.filter(activo=True)
        self.fields['tipo_procedimiento'].queryset = TipoProcedimiento.objects.filter(activo=True)

    def clean(self):
        cleaned_data = super().clean()
        oficial_entrega = cleaned_data.get('oficial_entrega')
        oficial_recibe = cleaned_data.get('oficial_recibe')
        
        # Validación: el oficial que entrega no puede ser el mismo que recibe
        if oficial_entrega and oficial_recibe and oficial_entrega == oficial_recibe:
            raise forms.ValidationError("El oficial que entrega no puede ser el mismo que recibe.")
        
        return cleaned_data


class FormularioCustodiaForm(forms.ModelForm):
    class Meta:
        model = FormularioCustodia
        fields = [
            'nro_hash',
            'caratula',
            'sumario',
            'juzgado_fiscalia',
            'secretaria',
            'otra_informacion',
            'identificacion_material',
            'breve_descripcion',
            'fecha_hora_incidente',
            'nro_control',
            'nro_orden',
        ]
        widgets = {
            'nro_hash': forms.Select(attrs={'class': 'form-select'}),
            'caratula': forms.TextInput(attrs={'class': 'form-control'}),
            'sumario': forms.TextInput(attrs={'class': 'form-control'}),
            'juzgado_fiscalia': forms.TextInput(attrs={'class': 'form-control'}),
            'secretaria': forms.TextInput(attrs={'class': 'form-control'}),
            'otra_informacion': forms.Textarea(attrs={'class': 'form-control', 'rows': 2}),
            'identificacion_material': forms.Textarea(attrs={'class': 'form-control', 'rows': 2}),
            'breve_descripcion': forms.Textarea(attrs={'class': 'form-control', 'rows': 2}),
            'fecha_hora_incidente': forms.DateTimeInput(attrs={'class': 'form-control', 'type': 'datetime-local'}),
            'nro_control': forms.NumberInput(attrs={'class': 'form-control'}),
            'nro_orden': forms.NumberInput(attrs={'class': 'form-control'}),
        }

    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        self.fields['nro_hash'].queryset = FormularioHash.objects.filter(estado='FINALIZADO')


class PersonalCustodiaForm(forms.ModelForm):
    class Meta:
        model = PersonalCustodia
        fields = ['oficial', 'funcion', 'descripcion', 'observaciones']
        widgets = {
            'oficial': forms.Select(attrs={'class': 'form-select'}),
            'funcion': forms.TextInput(attrs={'class': 'form-control'}),
            'descripcion': forms.Textarea(attrs={'class': 'form-control', 'rows': 2}),
            'observaciones': forms.Textarea(attrs={'class': 'form-control', 'rows': 2}),
        }

    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        self.fields['oficial'].queryset = Oficial.objects.filter(activo=True)


class QuickOficialForm(forms.ModelForm):
    """Formulario rápido para agregar oficiales"""
    es_civil = forms.BooleanField(
        required=False, 
        initial=False,
        label="Es Civil (sin jerarquía)",
        widget=forms.CheckboxInput(attrs={'class': 'form-check-input'})
    )
    
    class Meta:
        model = Oficial
        fields = ['legajo', 'nombre', 'jerarquia', 'destino', 'es_civil']
        widgets = {
            'legajo': forms.TextInput(attrs={'class': 'form-control form-control-sm', 'placeholder': 'Ej: 12345'}),
            'nombre': forms.TextInput(attrs={'class': 'form-control form-control-sm', 'placeholder': 'Nombre completo'}),
            'jerarquia': forms.Select(attrs={'class': 'form-select form-select-sm'}),
            'destino': forms.Select(attrs={'class': 'form-select form-select-sm'}),
        }
        labels = {
            'legajo': 'Legajo/DNI',
            'nombre': 'Nombre Completo',
            'jerarquia': 'Jerarquía/Rango',
            'destino': 'Destino/Unidad',
        }

    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        self.fields['jerarquia'].queryset = Jerarquia.objects.all().order_by('orden')
        self.fields['destino'].queryset = Destino.objects.filter(activo=True).order_by('nombre')
        
        # Hacer jerarquía opcional cuando es civil
        self.fields['jerarquia'].required = False
        
    def clean(self):
        cleaned_data = super().clean()
        es_civil = cleaned_data.get('es_civil', False)
        jerarquia = cleaned_data.get('jerarquia')
        
        if not es_civil and not jerarquia:
            raise forms.ValidationError("Debe seleccionar una jerarquía o marcar como civil.")
        
        if es_civil and jerarquia:
            raise forms.ValidationError("Un civil no puede tener jerarquía asignada.")
        
        return cleaned_data

    def save(self, commit=True):
        oficial = super().save(commit=False)
        
        # Si es civil, no asignar jerarquía
        if self.cleaned_data.get('es_civil', False):
            oficial.jerarquia = None
        
        if commit:
            oficial.save()
        return oficial


class QuickJerarquiaForm(forms.ModelForm):
    """Formulario rápido para agregar jerarquías"""
    class Meta:
        model = Jerarquia
        fields = ['nombre', 'abreviatura', 'orden']
        widgets = {
            'nombre': forms.TextInput(attrs={'class': 'form-control form-control-sm', 'placeholder': 'Ej: Teniente Coronel'}),
            'abreviatura': forms.TextInput(attrs={'class': 'form-control form-control-sm', 'placeholder': 'Ej: Tte. Crl.'}),
            'orden': forms.NumberInput(attrs={'class': 'form-control form-control-sm', 'placeholder': 'Orden jerárquico'}),
        }
        labels = {
            'nombre': 'Nombre de la Jerarquía',
            'abreviatura': 'Abreviatura',
            'orden': 'Orden Jerárquico',
        }

    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        # Sugerir el siguiente orden disponible
        if not self.instance.pk:
            max_orden = Jerarquia.objects.aggregate(max_orden=forms.models.Max('orden'))['max_orden']
            self.fields['orden'].initial = (max_orden or 0) + 1


class QuickDestinoForm(forms.ModelForm):
    """Formulario rápido para agregar destinos"""
    class Meta:
        model = Destino
        fields = ['nombre']
        widgets = {
            'nombre': forms.TextInput(attrs={'class': 'form-control form-control-sm', 'placeholder': 'Ej: Comisaría 1ra'}),
        }
        labels = {
            'nombre': 'Nombre del Destino/Unidad',
        }
