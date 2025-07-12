from django import forms
from .models import FormularioHash, Oficial, TipoProcedimiento, FormularioCustodia, PersonalCustodia

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
