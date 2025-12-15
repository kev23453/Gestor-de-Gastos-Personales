ALTER TABLE Categoria ADD CONSTRAINT FK_usuarios foreign key (id_usuario) references usuarios(id)
ALTER TABLE Gasto ADD CONSTRAINT FK_Categoria FOREIGN KEY (CategoriaId) REFERENCES Categoria(id_categoria),
ALTER TABLE Gasto ADD CONSTRAINT FK_MetodoPago FOREIGN KEY (metodoPagoId) REFERENCES metodoPago(id),
ALTER TABLE Gasto ADD CONSTRAINT FK_Usuarios FOREIGN KEY (usuarioId) REFERENCES usuarios(id),