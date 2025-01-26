import { Lavanderia } from "./lavanderia.model";

export interface Servicio {
	idServicio: number;
	nombreServicio: string;
	descripcion: string;
	precio: number;
	idLavanderia: number;
	lavanderia: Lavanderia;  // Relación con el modelo Lavanderia
}
