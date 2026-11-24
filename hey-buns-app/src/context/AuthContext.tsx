import { createContext, ReactNode, useContext, useEffect, useState } from "react";
import * as SecureStore from "expo-secure-store";
import { authService } from "../services/authService";
import { AuthRespuesta, Usuario } from "../types";

interface AuthContextType {
  usuario: Usuario | null;
  cargando: boolean;
  login: (correo: string, password: string) => Promise<void>;
  registro: (nombre: string, correo: string, password: string) => Promise<void>;
  logout: () => Promise<void>;
}

const AuthContext = createContext<AuthContextType>(null as any);

export const useAuth = () => useContext(AuthContext);

export function AuthProvider({ children }: { children: ReactNode }) {
  const [usuario, setUsuario] = useState<Usuario | null>(null);
  const [cargando, setCargando] = useState(true);

  // Al abrir la app revisa si ya había una sesión guardada
  useEffect(() => {
    (async () => {
      try {
        const guardado = await SecureStore.getItemAsync("usuario");
        if (guardado) setUsuario(JSON.parse(guardado));
      } finally {
        setCargando(false);
      }
    })();
  }, []);

  const guardarSesion = async (respuesta: AuthRespuesta) => {
    const { token, ...datos } = respuesta;
    await SecureStore.setItemAsync("token", token);
    await SecureStore.setItemAsync("usuario", JSON.stringify(datos));
    setUsuario(datos);
  };

  const login = async (correo: string, password: string) => {
    await guardarSesion(await authService.login(correo, password));
  };

  const registro = async (nombre: string, correo: string, password: string) => {
    await guardarSesion(await authService.registro(nombre, correo, password));
  };

  const logout = async () => {
    await SecureStore.deleteItemAsync("token");
    await SecureStore.deleteItemAsync("usuario");
    setUsuario(null);
  };

  return (
    <AuthContext.Provider value={{ usuario, cargando, login, registro, logout }}>
      {children}
    </AuthContext.Provider>
  );
}
