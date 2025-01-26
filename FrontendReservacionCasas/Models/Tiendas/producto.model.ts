import { Tienda } from "./tienda.model";

export interface Producto {
	idProducto: number;
	nombre: string;
	precio: number;
	idTienda: number;
	tienda: Tienda;  // Relación con el modelo Tienda
}
