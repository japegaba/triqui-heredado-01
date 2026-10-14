# Propuesta de arquitectura modular para Triqui

El diagnóstico del programa confirma que el problema principal no es solo la validación de entrada, sino la falta de separación entre reglas del juego, flujo de partida e interfaz. A partir de los estándares del proyecto, dos arquitecturas son factibles y compatibles con una solución .NET modular.

## Opción 1: arquitectura por capas del dominio

### Proyectos y responsabilidades

- Triqui.Core
  - Models/Tablero: representa el estado del tablero y la posición de cada casilla.
  - Models/Jugada: encapsula la acción de marcar una posición con un símbolo de jugador.
  - Models/ResultadoPartida: almacena el estado final de la partida, ganador y empate.
  - Services/PartidaService: coordina turnos, valida la jugada y determina el estado del juego.
  - Services/EvaluadorVictoria: calcula si existe una línea vencedora en filas, columnas y diagonales.
  - Validators/ValidadorJugada: comprueba rango, ocupación, turno y condiciones del tablero.
  - Interfaces/IJugador: define el contrato para un participante del juego.
  - Interfaces/ITablero: define el acceso al tablero sin acoplarlo a la consola.
  - Interfaces/IResultadoPartida: expone la información final de la partida.
- Triqui.Console
  - UI/ConsolaJuego: gestiona el ciclo de partida y la salida al usuario.
  - UI/ControladorPartida: orquesta la interacción entre la consola y la lógica del dominio.
  - UI/EntradaConsola: lee la casilla del usuario y devuelve errores de validación en español.
  - UI/SalidaConsola: muestra el tablero, mensajes y estado final del juego.
- Triqui.Tests
  - Tests/PartidaServiceTests: verifica turnos, reglas y fin de partida.
  - Tests/EvaluadorVictoriaTests: cubre filas, columnas y diagonales.
  - Tests/ValidadorJugadaTests: valida entradas inválidas, casillas ocupadas y errores esperados.

### Dependencias entre proyectos y clases

Triqui.Console --> Triqui.Core
Triqui.Tests --> Triqui.Core

Triqui.Console
  ├─> UI/ConsolaJuego
  │    ├─> UI/ControladorPartida
  │    ├─> UI/EntradaConsola
  │    └─> UI/SalidaConsola
  │
  └─> Triqui.Core
        ├─> Services/PartidaService
        │    ├─> Validators/ValidadorJugada
        │    ├─> Services/EvaluadorVictoria
        │    ├─> Models/Tablero
        │    ├─> Models/Jugada
        │    └─> Models/ResultadoPartida
        └─> Interfaces/IJugador
             ├─> Interfaces/ITablero
             └─> Interfaces/IResultadoPartida

Triqui.Tests
  └─> Triqui.Core
        ├─> Services/PartidaService
        ├─> Services/EvaluadorVictoria
        └─> Validators/ValidadorJugada

### Cuándo encaja bien

Esta opción funciona mejor cuando se desea una migración pequeña, una separación clara del dominio y una base de pruebas sólida sin introducir demasiada infraestructura nueva. Mantiene la lógica de juego en un único proyecto y deja la consola como adaptador de entrada y salida.

## Opción 2: arquitectura hexagonal con aplicación y adaptadores

### Proyectos y responsabilidades

- Triqui.Domain
  - Models/Tablero, Jugada, ResultadoPartida: representan la regla del juego como fuente de verdad del estado.
  - Services/EvaluadorVictoria, ReglasPartida: calculan victorias, empates y condiciones de fin del juego.
  - Interfaces/IJugador, ITablero, IResultadoPartida: definen contratos para dependencias externas.
- Triqui.Application
  - Services/PartidaService: orquesta la lógica de turno y evolución de la partida.
  - Services/ControladorPartida: coordina validaciones y actualización del estado.
  - Ports/IServicioDeEntrada, IServicioDeSalida: abstraen la forma en que el usuario interactúa con el juego.
  - UseCases/IniciarPartida, RealizarJugada: expresan los casos de uso del negocio.
- Triqui.AI
  - Services/JugadorComputadora: encapsula la lógica de un jugador controlado por la computadora.
  - Strategies/EstrategiaSimple, EstrategiaMinimax: deciden la jugada recomendada según el tablero.
  - Adapters/AdaptadorJugadorIA: conecta la inteligencia artificial con la interfaz de jugador del dominio.
- Triqui.Console
  - UI/ConsolaJuego: presenta el flujo para humanos en terminal.
  - Adapters/AdaptadorEntradaConsola, AdaptadorSalidaConsola: implementan los puertos de entrada y salida.
- Triqui.Tests
  - Tests/ReglasPartidaTests, PartidaServiceTests, JugadorComputadoraTests: validan dominio, casos de uso y comportamiento de la IA.

### Dependencias entre proyectos y clases

Triqui.Console --> Triqui.Application
Triqui.Console --> Triqui.Domain
Triqui.AI --> Triqui.Application
Triqui.AI --> Triqui.Domain
Triqui.Tests --> Triqui.Domain
Triqui.Tests --> Triqui.Application
Triqui.Tests --> Triqui.AI

Triqui.Console
  ├─> UI/ConsolaJuego
  │    ├─> Adapters/AdaptadorEntradaConsola
  │    ├─> Adapters/AdaptadorSalidaConsola
  │    └─> Triqui.Application
  │         ├─> Services/PartidaService
  │         ├─> Services/ControladorPartida
  │         ├─> UseCases/IniciarPartida
  │         └─> UseCases/RealizarJugada
  │
  └─> Triqui.Domain
        ├─> Services/EvaluadorVictoria
        ├─> Services/ReglasPartida
        ├─> Models/Tablero
        └─> Interfaces/IJugador

Triqui.AI
  ├─> Services/JugadorComputadora
  │    ├─> Strategies/EstrategiaSimple
  │    ├─> Strategies/EstrategiaMinimax
  │    └─> AdaptadorJugadorIA
  └─> Triqui.Domain
        ├─> Interfaces/IJugador
        └─> Models/Tablero

Triqui.Application
  └─> Triqui.Domain
        ├─> Services/EvaluadorVictoria
        ├─> Models/ResultadoPartida
        └─> Interfaces/IResultadoPartida

### Cuándo encaja bien

Esta opción se recomienda cuando se desea preparar el juego para crecimiento: varios tipos de jugador, inteligencia artificial, pruebas de integración y una UI que pueda cambiar sin tocar la lógica principal. Introduce más proyectos y más contratos, pero deja el sistema más extensible y mantenible.

## Comparativa

| Criterio | Opción 1: capas del dominio | Opción 2: hexagonal con aplicación y adaptadores |
| --- | --- | --- |
| Facilidad para probar las reglas | Alta, porque la lógica del juego está centralizada y se prueba sin depender de consola. | Muy alta, porque las reglas y los casos de uso están separados de los adaptadores y se pueden probar de forma aislada. |
| Facilidad para agregar un jugador controlado por la computadora | Media, se puede añadir una implementación de IJugador, pero requiere más adaptación desde la capa de consola. | Alta, porque la IA vive en un proyecto independiente y se conecta mediante interfaces y puertos. |
| Cantidad de archivos | Baja a media, con menos proyectos y menos clases auxiliares. | Media a alta, por la introducción de proyectos, ports, adapters y estrategias de IA. |
| Esfuerzo de la migración | Bajo, porque encaja mejor con la estructura actual y requiere menos reordenamiento. | Medio a alto, porque exige crear más capas, contratos y organización de dependencias. |

Recomendación: elegir la Opción 1 como base inicial para no romper el proyecto actual y para formalizar la separación entre dominio y consola con mínimo riesgo.
Cuando el juego empiece a evolucionar hacia IA, multijugador o más modalidades, la Opción 2 será la evolución natural y más sostenible.

## Decisión del equipo
Opción 1