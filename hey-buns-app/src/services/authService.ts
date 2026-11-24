import api from "./api";
import { AuthRespuesta } from "../types";

export const authService = {
  async login(correo: string, password: string): Promise<AuthRespuesta> {
    const res = await api.post<AuthRespuesta>("/auth/login", { correo, password });
    return res.data;
  },

  async registro(nombre: string, correo: string, password: string): Promise<AuthRespuesta> {
    const res = await api.post<AuthRespuesta>("/auth/registro", { nombre, correo, password });
    return res.data;
  },
};
