import axios from "axios";
import * as SecureStore from "expo-secure-store";

// Android emulador: http://10.0.2.2:5000/api  |  celular con Expo Go: IP de la computadora
export const API_URL = process.env.EXPO_PUBLIC_API_URL ?? "http://10.0.2.2:5000/api";

const api = axios.create({ baseURL: API_URL, timeout: 10000 });

// Agrega el token JWT en cada petición
api.interceptors.request.use(async (config) => {
  const token = await SecureStore.getItemAsync("token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default api;
