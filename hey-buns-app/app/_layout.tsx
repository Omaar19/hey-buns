import { useEffect } from "react";
import { Slot, useRouter, useSegments } from "expo-router";
import { AuthProvider, useAuth } from "../src/context/AuthContext";

// Manda al login si no hay sesión, y a la pantalla principal si ya la hay
function Guardia() {
  const { usuario, cargando } = useAuth();
  const segments = useSegments();
  const router = useRouter();

  useEffect(() => {
    if (cargando) return;
    const enAuth = segments[0] === "(auth)";
    if (!usuario && !enAuth) {
      router.replace("/login");
    } else if (usuario && enAuth) {
      router.replace("/");
    }
  }, [usuario, cargando, segments]);

  return <Slot />;
}

export default function RootLayout() {
  return (
    <AuthProvider>
      <Guardia />
    </AuthProvider>
  );
}
