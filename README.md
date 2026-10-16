# Triqui

Juego de triqui (tres en línea) para dos jugadores en la consola.

## Contexto

Este proyecto lo escribió un desarrollador que ya no está en el equipo. Se usó en la feria de la empresa y "funciona", pero nadie más lo ha tocado. Ahora el equipo lo hereda y tiene que seguir desarrollándolo: la próxima versión debe tener un rival que juegue solo.

## Cómo ejecutarlo

```powershell
dotnet run
```

Para compilar la solución desde su carpeta raíz:

```powershell
dotnet build
```

## Cómo se juega

- Juegan X y O por turnos.
- Cada jugador escribe el número de la casilla (1 a 9) donde quiere jugar.
- Gana quien complete tres en línea: horizontal, vertical o diagonal.
- Al terminar, el juego pregunta si quieren otra partida y lleva el marcador.

## Validación de entrada

- La aplicación comprueba que la casilla no esté vacía ni sea no numérica.
- Si el usuario introduce un valor decimal, fuera de rango o no válido, el sistema lo avisa en español y vuelve a pedir la casilla.
- Las casillas ya ocupadas también se rechazan con un aviso claro para continuar la partida sin romper la ejecución.
- Antes de marcar una casilla, el dominio comprueba el rango, que esté disponible y que la partida no haya terminado.
