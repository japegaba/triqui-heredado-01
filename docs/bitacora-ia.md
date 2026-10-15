
# Bitácora de cambios con IA

Registro de cada cambio hecho con ayuda de la IA: qué cambió, por qué, cómo se verificó y qué decidió el equipo.

## 2026-10-14 17:46 · Etapa 1.1 · Arquitectura propuesta para modularizar Triqui
- Cambio: se creó el documento de arquitectura con dos propuestas de reorganización en solución .NET modular, describiendo proyectos, carpetas, responsabilidades, dependencias y comparación.
- Motivo: el diagnóstico identifica la ausencia de separación entre reglas del juego, flujo y consola, y los estándares del proyecto exigen modularidad y trazabilidad de decisiones.
- Archivos: docs/arquitectura.md
- Verificación: revisión del contenido y validación de que el documento cumple el formato solicitado en español, con diagramas textuales, tabla comparativa y cierre con recomendación.
- Decisión del equipo: Opción 1

## 2026-10-14 17:47 · Etapa 3.1 · Plan de migración por etapas para la opción elegida
- Cambio: se creó el plan de trabajo para migrar a la arquitectura de la Opción 1, con etapas numeradas 3.1 a 3.4, tiempos máximos de 15 minutos y una verificación específica por fallo del diagnóstico.
- Motivo: la decisión del equipo confirma la opción 1 y se requiere un plan gradual para llegar a esa arquitectura sin interrumpir la funcionalidad del juego.
- Archivos: docs/plan.md
- Verificación: revisión del documento para confirmar que cada etapa tiene objetivo, archivos, hallazgo del diagnóstico y forma de comprobación, y que la etapa 3.1 crea la solución con biblioteca y consola sin cambiar el comportamiento del juego.
- Decisión del equipo: Aceptado

## 2026-10-15 14:42 · Etapa 3.1 · Solución modular con dominio y consola
- Cambio: se creó la solución .NET con los proyectos Triqui.Core y Triqui.Console; el contenido de Program.cs se movió al proyecto de consola sin alterar su lógica y la biblioteca quedó con modelos, validación y servicios mínimos alineados con la arquitectura elegida.
- Motivo: la etapa 3.1 del plan exige separar el dominio de la interfaz manteniendo el comportamiento actual y preparar la base para la siguiente etapa de validación.
- Archivos: Triqui.slnx, Triqui.Core/**, Triqui.Console/Program.cs, Triqui.Console/Triqui.Console.csproj
- Verificación: compilación con dotnet build en la raíz del proyecto, que finalizó correctamente con 2 advertencias de nulabilidad heredadas del código original y sin cambiar el flujo del juego.
- Decisión del equipo: Aceptado

## 2026-10-15 17:01 · Etapa 3.2 · Validación segura de entradas
- Cambio: se aisló la lectura y validación de la casilla en un helper de la consola para controlar entradas vacías, no numéricas, fuera de rango y casillas ocupadas antes de actualizar el tablero; además se documentó ese flujo en el README.
- Motivo: el diagnóstico D-01 identifica la dependencia directa de int.Parse(Console.ReadLine()) sin protección, y la etapa 3.2 exige corregir ese punto sin avanzar a la validación de estado del juego ni a la lógica de victoria.
- Archivos: Triqui.Console/Program.cs, README.md
- Verificación: compilación con dotnet build desde la raíz del proyecto y comprobación del flujo de entrada para valores vacíos, no numéricos y fuera de rango, que ahora muestran mensajes en español y repiten la solicitud de casilla.
- Decisión del equipo: Aceptado