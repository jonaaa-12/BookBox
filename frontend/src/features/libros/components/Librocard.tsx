// src/features/libros/components/LibroCard.tsx
import type { Libro } from '../types/libro.types'

export function LibroCard({ id, titulo }: Libro) {
    return (
        <article className="bg-lb-surface border border-lb-line rounded-xl p-5 flex flex-col justify-between hover:border-lb-green transition-colors shadow-sm">
            <div>
                <div className="flex items-center justify-between mb-3">
                    <span className="bg-lb-blue/10 text-lb-blue text-xs font-semibold px-2.5 py-1 rounded-md border border-lb-blue/20">
                        ID Libro #{id}
                    </span>
                    <span className="text-xs text-lb-muted font-medium">Catálogo Base</span>
                </div>
                
                <h3 className="text-lg font-bold text-white mb-5">
                    {titulo}
                </h3>
            </div>

            <button 
                disabled 
                className="w-full bg-lb-bg/80 text-lb-muted text-sm font-medium py-2.5 px-4 rounded-lg cursor-not-allowed border border-lb-line"
            >
                🔒 Marcar como Leído (Requiere Sesión)
            </button>
        </article>
    );
} 