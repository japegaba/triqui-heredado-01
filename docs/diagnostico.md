# Diagnóstico del programa

## Resumen
- El programa implementa un juego de Triqui en consola para dos jugadores humanos.
- Recorre partidas en bucle, valida jugadas antes de actualizar el tablero, lleva marcador y permite iniciar otra partida.
- Los hallazgos siguientes describen el estado inicial del programa heredado y registran la resolución aplicada en las etapas posteriores.

## Hallazgos

| ID | Hallazgo inicial | Dónde | Tipo | Impacto | Resolución y estado actual |
| --- | --- | --- | --- | --- | --- |
| D-01 | La lectura utilizaba `int.Parse(Console.ReadLine())` sin validar la entrada; un valor vacío o no numérico podía terminar la aplicación. | Punto de entrada y lectura de la jugada | Falla | Alto | **Resuelto en etapa 3.2.** `TryLeerPosicion` usa `int.TryParse`, muestra un aviso y vuelve a pedir la casilla ante entradas vacías o inválidas. |
| D-04 | La validación inicial solo consideraba rango y ocupación, y ocurría después de leer la entrada sin protección. | Flujo de lectura y validación de jugadas | Calidad | Medio | **Resuelto en etapa 3.3.** `PartidaService.ValidarJugada` comprueba que la partida siga activa y delega en `ValidadorJugada` la comprobación de rango y disponibilidad antes de marcar el tablero. |
| D-05 | La documentación no explicaba las reglas, el flujo de partida ni las validaciones, por lo que el comportamiento era difícil de mantener. | README y documentación de arquitectura y diagnóstico | Documentación | Bajo | **Resuelto en etapa 3.4.** El README describe uso, reglas, flujo, límites y validación; la arquitectura documenta la implementación actual y este diagnóstico conserva los hallazgos junto con su estado. |
| D-06 | La comprobación de victoria omitía la diagonal secundaria (casillas 3, 5 y 7). | Evaluación de victoria | Falla | Alto | **Resuelto en etapa 3.3.** `EvaluadorVictoria` contempla ambas diagonales; `pruebas/x-gana-diagonal-357.txt` registra el caso de referencia. |

## Observaciones adicionales

- La lógica de victoria vigente comprueba filas, columnas y las diagonales 1-5-9 y 3-5-7. El empate se determina cuando el tablero está lleno y no hay ganador.
- El dominio está separado de la interacción con la terminal en `Triqui.Core` y `Triqui.Console`; el flujo y las responsabilidades actuales se detallan en `docs/arquitectura.md`.
- Los archivos de entrada de referencia están en `pruebas/`; no se modificaron como parte de la etapa documental.
