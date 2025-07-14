# Hash Application Tests

Este proyecto contiene pruebas unitarias para la aplicación Hash, diseñadas para prevenir regresiones en el código.

## Estructura de Pruebas

### BEArchivoTests.cs
Pruebas para la clase `BEArchivo` que incluyen:
- Verificación de cálculo de peso en diferentes unidades (KB, MB, GB)
- Formateo correcto de cadenas de peso
- Inicialización correcta de propiedades
- Casos límite y valores extremos

### FormularioHashTests.cs
Pruebas para la clase `Formulario_Hash` que incluyen:
- Conteo correcto de archivos por tipo (imágenes, clips, audio, texto, varios)
- Asignación correcta de números de orden
- Cálculo de peso total
- Manejo de listas vacías y casos mixtos
- Validación de lógica de categorización por extensión

### HashUtilitiesTests.cs
Pruebas para la nueva clase utilitaria `HashUtilities` que incluyen:
- Cálculo de hash SHA256 con validación de hashes conocidos
- Manejo de errores para archivos inexistentes o rutas inválidas
- Validación de formato de hash SHA256
- Pruebas de consistencia y diferenciación de hashes
- Validación de caracteres hexadecimales y longitud correcta

### HashCalculationTests.cs
Pruebas para el algoritmo de cálculo de hash SHA256 que incluyen:
- Verificación de hashes conocidos para contenido específico
- Consistencia en el cálculo de hash para el mismo contenido
- Diferenciación correcta para contenido diferente
- Manejo de archivos grandes
- Formato correcto del hash resultante
- Manejo de errores para archivos inexistentes

## Ejecución de Pruebas

Las pruebas están configuradas para ejecutarse con MSTest en Visual Studio o mediante la línea de comandos:

```bash
# Restaurar paquetes NuGet
nuget restore

# Ejecutar pruebas
vstest.console.exe Hash.Tests\bin\Debug\Hash.Tests.dll
```

## Objetivo

Estas pruebas están diseñadas para:
1. **Prevenir regresiones**: Detectar automáticamente cambios que rompan funcionalidad existente
2. **Validar lógica crítica**: Asegurar que los cálculos de hash y categorización de archivos funcionen correctamente
3. **Documentar comportamiento esperado**: Servir como documentación ejecutable del comportamiento del sistema
4. **Facilitar refactoring**: Permitir cambios en el código con confianza

## Notas Importantes

- Las pruebas de hash utilizan archivos temporales para evitar dependencias externas
- Los tests de categorización validan la lógica de extensiones de archivo hardcodeada
- Se incluyen pruebas para casos límite y valores extremos
- Las pruebas están diseñadas para ser independientes y pueden ejecutarse en cualquier orden

## Mejoras Incluidas

### Nueva Clase HashUtilities
Se ha agregado una nueva clase utilitaria `HashUtilities` que proporciona:
- Método estático `CalcularHashSHA256()` para cálculo de hash reutilizable
- Método `EsHashSHA256Valido()` para validación de formato de hash
- Mejor separación de responsabilidades y código más testeable
- Manejo robusto de errores con excepciones específicas

Esta clase permite que el código de cálculo de hash sea más fácil de probar y reutilizar en toda la aplicación.