# Estándares de desarrollo .NET para Triqui

## Estructura y modularidad

1. Un proyecto .NET debe reflejar responsabilidades: `Triqui.Core` para reglas del juego, `Triqui.Console` para entrada/salida y `Triqui.Tests` para pruebas.
2. Cada clase debe vivir en un archivo con el mismo nombre y una sola responsabilidad principal.
3. Un archivo no debe mezclar tablero, validación y flujo de consola en la misma clase.
4. Las carpetas deben agrupar por dominio: `Models`, `Services`, `Validators`, `UI` y `Tests`.
5. La lógica de victoria debe estar en un servicio independiente; no calcularse dentro del bucle principal.
6. Si un método supera 20 líneas o tiene dos razones de cambio, debe extraerse a otro método o clase.

## Principios de diseño

7. SRP: cada clase debe tener una sola razón de cambio; `Program.cs` no debe validar entrada, decidir el ganador y controlar la partida a la vez.
8. OCP: la lógica del juego debe permitir añadir una nueva regla o una diagonal extra sin reescribir la lógica principal del flujo.
9. LSP: cualquier implementación de tablero o jugador debe poder reemplazar la base sin romper la partida.
10. ISP: usa interfaces pequeñas como `IEntrada`, `ITablero` y `IResultadoPartida`; no dependas de interfaces gigantescas.
11. DIP: la lógica del juego debe depender de abstracciones y no de `Console.ReadLine()` ni `Console.WriteLine()`.
12. La UI debe presentar mensajes de error claros y devolver el control al flujo principal; no debe ocultar fallas de validación.

## Nombres e idioma

13. Los nombres de clases, métodos y propiedades deben ser explícitos y consistentes; evita abreviaturas como `pos`, `num` o `tmp`.
14. Usa `PascalCase` para clases, `camelCase` para parámetros y `PascalCase` para propiedades públicas.
15. Los métodos deben comenzar con verbos claros: `ValidarJugada`, `ObtenerCasilla`, `HayGanador`.
16. El idioma del dominio debe ser consistente; usa español para el juego y evita mezclar español e inglés en la misma API.

## Errores y validación de entradas

17. Cada entrada de consola debe validarse antes de usarse; no se hace `int.Parse(Console.ReadLine())` sin protección.
18. Si la entrada es nula, vacía o no numérica, el programa debe avisar al usuario y volver a pedir la casilla.
19. La validación de casilla debe comprobar rango, ocupación y estado del juego antes de actualizar el tablero.
20. Las excepciones esperadas deben controlarse en el punto de entrada y nunca propagarse sin mensaje.
21. Los métodos deben devolver resultados o errores explícitos para casos esperados; no ocultar errores con valores nulos.
22. La victoria debe evaluarse en todas las líneas válidas del tablero, incluyendo diagonales.
23. Los mensajes de error deben ser cortos, accionables y en español: "La casilla está ocupada. Elija otra.".

## Documentación

24. Cada clase pública debe tener un resumen XML con `<summary>`.
25. Cada método público con parámetros y retorno debe documentarse con `<param>` y `<returns>` cuando su comportamiento no sea evidente.
26. El README debe explicar reglas del juego, flujo de partida, validaciones y cómo ejecutar o depurar el proyecto.
27. Los comentarios deben describir intención; no repetir el código ni dejar notas como "TODO: organizar esto algun dia".
28. Si un comportamiento es crítico, debe documentarse en el README y en el código: validación, victoria, empate y límites del tablero.

## Forma de trabajar

29. Una solicitud equivale a una sola etapa del plan: diagnosticar, cambiar, revisar y validar; no mezclar correcciones sin relación.
30. No ejecutar `dotnet run` para comprobar cada ajuste; usa compilación y revisión del flujo cuando sea necesario.
31. Antes de editar, identifica la causa raíz y el punto exacto del fallo en lugar de corregir síntomas aislados.
32. Las pruebas o verificaciones deben cubrir el caso que originó el problema y una regresión relevante.
33. Si un cambio afecta la lógica de victoria, se valida en filas, columnas y diagonales en la misma revisión.
34. Los cambios deben ser pequeños y verificables; no hacer refactorizaciones grandes sin un objetivo claro.
35. Cuando detectes una falla de entrada o de reglas, corrígela en la capa apropiada y no la escondas en la interfaz.

## Bitácora de cambios con IA

Después de cada cambio que hagas en el proyecto, agrega una entrada al final de docs/bitacora-ia.md con este formato:

## AAAA-MM-DD HH:mm · Etapa X.Y · Título corto
- Cambio: qué se hizo, en una o dos líneas.
- Motivo: el hallazgo del diagnóstico o la etapa del plan que lo pide.
- Archivos: los archivos creados o modificados.
- Verificación: cómo se comprobó.
- Decisión del equipo: pendiente

Para la fecha y la hora, ejecuta en la terminal: Get-Date -Format "yyyy-MM-dd HH:mm"
No borres ni cambies entradas anteriores. Si no hiciste cambios, no agregues entrada.
## Verificación

- Cada etapa se verifica con dotnet build, sin errores, y con las partidas de prueba de pruebas/README.md.
- No modifiques los archivos de la carpeta pruebas. Si una partida falla, se corrige el código, no la partida.
- No cambies los mensajes que ve el jugador (por ejemplo «Gana X» o el marcador final) si la etapa no lo pide.