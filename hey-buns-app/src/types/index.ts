export interface Usuario {
  id: number;
  nombre: string;
  correo: string;
  rol: string;
}

export interface AuthRespuesta extends Usuario {
  token: string;
}

export interface Producto {
  id: number;
  categoriaId: number;
  categoria: string;
  nombre: string;
  descripcion: string | null;
  precio: number;
  imagenUrl: string | null;
  disponible: boolean;
}
