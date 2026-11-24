import api from "./api";
import { Producto } from "../types";

export const productosService = {
  async listar(): Promise<Producto[]> {
    const res = await api.get<Producto[]>("/productos");
    return res.data;
  },
};
