import { Producto } from "./producto.model";

export interface Orden {
	id: number;
	orderDate: Date;
	totalAmount: number;
	productos: Producto[];  // Lista de productos en la orden
}
