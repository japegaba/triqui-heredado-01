namespace Triqui.Core.Validators;

/// <summary>
/// Describe el resultado de validar una jugada antes de modificar el tablero.
/// </summary>
public enum EstadoValidacionJugada
{
    Valida,
    PosicionFueraDeRango,
    CasillaOcupada,
    PartidaFinalizada
}
