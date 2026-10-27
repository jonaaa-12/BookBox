// src/components/LibroCard.tsx

// --- ZONA 1: EL CONTRATO (INTERFAZ) ---
// Actúa como un espejo exacto de nuestra tabla PostgreSQL / DTO en C#.
// Ajusta estos campos a las columnas reales de tu entidad Libro.
interface LibroProps {
    id: number;
    titulo: string;
}

// --- ZONA 2: LA FIRMA (EXPORTACIÓN NOMBRADA) ---
// Desestructuramos las props cumpliendo el contrato de LibroProps
export function LibroCard({ id, titulo }: LibroProps) {
    
    // --- ZONA 3: EL CEREBRO (LÓGICA) ---
    // (Vacío por los momentos, este componente es puramente presentacional)
    
    // --- ZONA 4: EL RENDERIZADO (JSX) ---
    return (
        <article className="book-card" style={{ border: '1px solid #ccc', padding: '1rem', margin: '1rem', borderRadius: '8px' }}>
            <small style={{ color: 'gray' }}>ID Libro: {id}</small>
            <h3>{titulo}</h3>
            
            {/* 
               En la Unidad 3, cuando tengamos sistema de Usuarios (IAM), 
               este botón llamará a la tabla transaccional Usuarios-Libros
               para registrar el libro como leído.
            */}
            <button disabled style={{ cursor: 'not-allowed' }}>
                Marcar como Leído (Próximamente)
            </button>
        </article>
    );
} 