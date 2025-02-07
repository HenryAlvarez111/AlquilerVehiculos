-- Crear la base de datos AlquilerAutos
CREATE DATABASE AlquilerAutos;
GO

-- Utilizar la base de datos recién creada
USE AlquilerAutos;
GO

CREATE TABLE Cliente
(
    IDCliente INT IDENTITY(1,1) PRIMARY KEY,
    Nombre     VARCHAR(100) NOT NULL,
    Telefono   VARCHAR(20)  NULL
);
GO
CREATE TABLE Vehiculo
(
    IDVehiculo INT IDENTITY(1,1) PRIMARY KEY,
    Marca      VARCHAR(50) NOT NULL,
    Modelo     VARCHAR(50) NOT NULL
);
GO

CREATE TABLE Contrato
(
    IDContrato  INT IDENTITY(1,1) PRIMARY KEY,
    IDCliente   INT NOT NULL,
    IDVehiculo  INT NOT NULL,
    FechaInicio DATE NOT NULL,
    FechaFin    DATE NOT NULL,

    CONSTRAINT FK_Contrato_Cliente 
        FOREIGN KEY (IDCliente) 
        REFERENCES Cliente (IDCliente),

    CONSTRAINT FK_Contrato_Vehiculo 
        FOREIGN KEY (IDVehiculo) 
        REFERENCES Vehiculo (IDVehiculo)
);
GO

INSERT INTO Cliente (Nombre, Telefono)
VALUES 
('Juan Pérez', '555-1234'),
('María López', '555-5678'),
('Carlos Ortega', '555-9012');
GO

INSERT INTO Vehiculo (Marca, Modelo)
VALUES
('Toyota', 'Corolla'),
('Ford', 'Focus'),
('Chevrolet', 'Spark');
GO

INSERT INTO Contrato (IDCliente, IDVehiculo, FechaInicio, FechaFin)
VALUES
(1, 1, '2025-01-01', '2025-01-10'),
(2, 2, '2025-02-05', '2025-02-15'),
(3, 3, '2025-03-10', '2025-03-20');
GO

CREATE OR ALTER PROCEDURE spCreateCliente
    @Nombre   VARCHAR(100),
    @Telefono VARCHAR(20)
AS
BEGIN
    INSERT INTO Cliente (Nombre, Telefono)
    VALUES (@Nombre, @Telefono);

    SELECT SCOPE_IDENTITY() AS NuevoIDCliente;
END;
GO

CREATE OR ALTER PROCEDURE spReadCliente
    @IDCliente INT = NULL
AS
BEGIN
    IF @IDCliente IS NULL
    BEGIN
        SELECT 
            IDCliente, 
            Nombre, 
            Telefono
        FROM Cliente;
    END
    ELSE
    BEGIN
        SELECT 
            IDCliente, 
            Nombre, 
            Telefono
        FROM Cliente
        WHERE IDCliente = @IDCliente;
    END
END;
GO

CREATE OR ALTER PROCEDURE spUpdateCliente
    @IDCliente INT,
    @Nombre    VARCHAR(100),
    @Telefono  VARCHAR(20)
AS
BEGIN
    UPDATE Cliente
    SET 
        Nombre   = @Nombre,
        Telefono = @Telefono
    WHERE IDCliente = @IDCliente;
END;
GO

CREATE OR ALTER PROCEDURE spDeleteCliente
    @IDCliente INT
AS
BEGIN
    DELETE FROM Cliente
    WHERE IDCliente = @IDCliente;
END;
GO

CREATE OR ALTER PROCEDURE spCreateVehiculo
    @Marca  VARCHAR(50),
    @Modelo VARCHAR(50)
AS
BEGIN
    INSERT INTO Vehiculo (Marca, Modelo)
    VALUES (@Marca, @Modelo);

    SELECT SCOPE_IDENTITY() AS NuevoIDVehiculo;
END;
GO

CREATE OR ALTER PROCEDURE spReadVehiculo
    @IDVehiculo INT = NULL
AS
BEGIN
    IF @IDVehiculo IS NULL
    BEGIN
        SELECT 
            IDVehiculo,
            Marca, 
            Modelo
        FROM Vehiculo;
    END
    ELSE
    BEGIN
        SELECT 
            IDVehiculo,
            Marca, 
            Modelo
        FROM Vehiculo
        WHERE IDVehiculo = @IDVehiculo;
    END
END;
GO

CREATE OR ALTER PROCEDURE spUpdateVehiculo
    @IDVehiculo INT,
    @Marca      VARCHAR(50),
    @Modelo     VARCHAR(50)
AS
BEGIN
    UPDATE Vehiculo
    SET 
        Marca  = @Marca,
        Modelo = @Modelo
    WHERE IDVehiculo = @IDVehiculo;
END;
GO
CREATE OR ALTER PROCEDURE spDeleteVehiculo
    @IDVehiculo INT
AS
BEGIN
    DELETE FROM Vehiculo
    WHERE IDVehiculo = @IDVehiculo;
END;
GO

CREATE OR ALTER PROCEDURE spCreateContrato
    @IDCliente  INT,
    @IDVehiculo INT,
    @FechaInicio DATE,
    @FechaFin    DATE
AS
BEGIN
    INSERT INTO Contrato (IDCliente, IDVehiculo, FechaInicio, FechaFin)
    VALUES (@IDCliente, @IDVehiculo, @FechaInicio, @FechaFin);

    SELECT SCOPE_IDENTITY() AS NuevoIDContrato;
END;
GO

CREATE OR ALTER PROCEDURE spReadContrato
    @IDContrato INT = NULL
AS
BEGIN
    IF @IDContrato IS NULL
    BEGIN
        SELECT 
            IDContrato,
            IDCliente,
            IDVehiculo,
            FechaInicio,
            FechaFin
        FROM Contrato;
    END
    ELSE
    BEGIN
        SELECT 
            IDContrato,
            IDCliente,
            IDVehiculo,
            FechaInicio,
            FechaFin
        FROM Contrato
        WHERE IDContrato = @IDContrato;
    END
END;
GO

CREATE OR ALTER PROCEDURE spUpdateContrato
    @IDContrato  INT,
    @IDCliente   INT,
    @IDVehiculo  INT,
    @FechaInicio DATE,
    @FechaFin    DATE
AS
BEGIN
    UPDATE Contrato
    SET 
        IDCliente   = @IDCliente,
        IDVehiculo  = @IDVehiculo,
        FechaInicio = @FechaInicio,
        FechaFin    = @FechaFin
    WHERE IDContrato = @IDContrato;
END;
GO

CREATE OR ALTER PROCEDURE spDeleteContrato
    @IDContrato INT
AS
BEGIN
    DELETE FROM Contrato
    WHERE IDContrato = @IDContrato;
END;
GO
