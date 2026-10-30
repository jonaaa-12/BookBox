// src/features/libros/components/CatalogoLibrosView.tsx
import { useState, useEffect } from 'react'
import type { Libro } from '../types/libro.types'
import { obtenerLibrosAsync } from '../api/librosApi'
import { LibroCard } from './Librocard'

export function CatalogoLibrosView() {
  // --- ZONA 3: CEREBRO DEL MÓDULO ---
  const [libros, setLibros] = useState<Libro[]>([])
  const [cargando, setCargando] = useState<boolean>(true)
  const [busqueda, setBusqueda] = useState<string>('')

  useEffect(() => {
    // Consumimos nuestra capa de servicio (api/librosApi.ts)
    obtenerLibrosAsync().then((datos) => {
      setLibros(datos)
      setCargando(false)
    })
  }, [])

  const librosFiltrados = libros.filter((libro) =>
    libro.titulo.toLowerCase().includes(busqueda.toLowerCase())
  )

  // --- ZONA 4: RENDERIZADO EXCLUSIVO DEL CATÁLOGO ---
  return (
    <div>
      {/* Barra Superior de Título y Acción Principal */}
      <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4 pb-6 mb-6 border-b border-lb-line">
        <div>
          <h1 className="text-2xl md:text-3xl font-bold text-white">Catálogo de Libros</h1>
          <p className="text-lb-text text-sm mt-1">
            Módulo desacoplado bajo arquitectura <code className="text-lb-blue font-mono">Vertical Slicing</code>.
          </p>
        </div>

        <button className="bg-lb-green hover:brightness-110 text-lb-bg font-semibold text-sm px-4 py-2.5 rounded-lg transition-all shadow-sm self-start sm:self-auto cursor-pointer">
          + Nuevo Libro
        </button>
      </div>

      {/* Buscador Reactivo */}
      <div className="mb-6 flex flex-col sm:flex-row sm:items-center justify-between gap-4">
        <input
          type="text"
          placeholder="🔍 Filtrar libros en el catálogo..."
          value={busqueda}
          onChange={(e) => setBusqueda(e.target.value)}
          className="w-full sm:max-w-md bg-lb-sidebar border border-lb-line rounded-lg px-4 py-2.5 text-sm text-white placeholder:text-lb-muted focus:outline-none focus:border-lb-green transition-colors"
        />
        <span className="text-xs text-lb-muted">
          Mostrando <strong className="text-white">{librosFiltrados.length}</strong> de {libros.length} registros
        </span>
      </div>

      {/* Renderizado Condicional y Rejilla */}
      {cargando ? (
        <div className="bg-lb-surface/50 border border-lb-line rounded-xl p-12 text-center">
          <p className="text-lb-green font-semibold animate-pulse text-lg">
            ⏳ Consultando capa de servicios (librosApi.ts)...
          </p>
        </div>
      ) : (
        <section className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-5">
          {librosFiltrados.map((libro) => (
            <LibroCard 
              key={libro.id} 
              id={libro.id} 
              titulo={libro.titulo} 
            />
          ))}
        </section>
      )}
    </div>
  )
} 