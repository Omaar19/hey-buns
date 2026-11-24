import { useEffect, useState } from "react";
import { ActivityIndicator, FlatList, StyleSheet, Text, View } from "react-native";
import { productosService } from "../../src/services/productosService";
import { obtenerMensajeError } from "../../src/utils/errores";
import { Producto } from "../../src/types";

export default function Menu() {
  const [productos, setProductos] = useState<Producto[]>([]);
  const [cargando, setCargando] = useState(true);
  const [error, setError] = useState("");

  const cargar = async () => {
    try {
      setError("");
      setProductos(await productosService.listar());
    } catch (e) {
      setError(obtenerMensajeError(e));
    } finally {
      setCargando(false);
    }
  };

  useEffect(() => {
    cargar();
  }, []);

  if (cargando) {
    return (
      <View style={estilos.centro}>
        <ActivityIndicator size="large" />
      </View>
    );
  }

  if (error) {
    return (
      <View style={estilos.centro}>
        <Text style={estilos.error}>{error}</Text>
      </View>
    );
  }

  return (
    <FlatList
      style={estilos.lista}
      data={productos}
      keyExtractor={(p) => p.id.toString()}
      refreshing={cargando}
      onRefresh={cargar}
      renderItem={({ item }) => (
        <View style={estilos.tarjeta}>
          <Text style={estilos.nombre}>{item.nombre}</Text>
          <Text style={estilos.categoria}>{item.categoria}</Text>
          {item.descripcion ? <Text>{item.descripcion}</Text> : null}
          <Text style={estilos.precio}>${item.precio}</Text>
        </View>
      )}
    />
  );
}

const estilos = StyleSheet.create({
  centro: { flex: 1, justifyContent: "center", alignItems: "center", padding: 24 },
  error: { textAlign: "center", fontSize: 16 },
  lista: { backgroundColor: "#fff" },
  tarjeta: { borderWidth: 1, borderColor: "#ccc", borderRadius: 6, padding: 12, marginHorizontal: 16, marginTop: 12 },
  nombre: { fontSize: 18, fontWeight: "bold" },
  categoria: { color: "#666", marginBottom: 4 },
  precio: { fontSize: 16, fontWeight: "bold", marginTop: 6 },
});
