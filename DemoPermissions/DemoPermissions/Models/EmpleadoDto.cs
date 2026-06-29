namespace DemoPermissions.Models;

public record EmpleadoDto(
    string Cedula,
    string Nombre,
    string Apellidos,
    string Correo,
    int Salario
);
