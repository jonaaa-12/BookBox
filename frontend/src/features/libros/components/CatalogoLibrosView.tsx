// frontend/src/features/libros/components/CatalogoLibrosView.tsx
import { useState, useEffect } from 'react'
import type { Libro } from '../types/libro.types'
import { obtenerLibrosAsync, extraerMensajeErrorBackend } from '../api/librosApi'
import { LibroCard } from './Librocard'
import { CrearLibroForm } from './CrearLibroForm'

export function CatalogoLibrosView() {
  const [libros, setLibros] = useState<Libro[]>([])
  const [cargando, setCargando] = useState<boolean>(true)
  const [errorRed, setErrorRed] = useState<string | null>(null)
  const [busqueda, setBusqueda] = useState<string>('')
  const [mostrarFormulario, setMostrarFormulario] = useState<boolean>(false)

  useEffect(() => {
    obtenerLibrosAsync()
      .then((datosDesdePostgres) => {
        setLibros(datosDesdePostgres)
        setErrorRed(null) 
      })
      .catch((error) => {
        console.error('Auditoría de Error en la Petición HTTP:', error)
        setErrorRed(extraerMensajeErrorBackend(error))
      })
      .finally(() => {
        setCargando(false)
      })
  }, [])

  // Actualización Inmutable en el Heap usando Spread Operator [...]
  const manejarLibroCreado = (nuevoLibroDesdeBD: Libro) => {
    setLibros((librosActuales) => [...librosActuales, nuevoLibroDesdeBD])
    setMostrarFormulario(false)
  }

  const librosFiltrados = libros.filter((libro) =>
    libro.title.toLowerCase().includes(busqueda.toLowerCase())
  )

  return (
    <div>
      <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4 pb-6 mb-6 border-b border-lb-line">
        <div>
          <h1 className="text-2xl md:text-3xl font-bold text-white">Catálogo de Libros</h1>
          <p className="text-lb-text text-sm mt-1">
            Conectado en tiempo real vía HTTP/CORS con <code className="text-lb-blue font-mono">PostgreSQL</code>.
          </p>
        </div>

        {!mostrarFormulario && (
          <button 
            onClick={() => setMostrarFormulario(true)}
            className="bg-lb-green hover:brightness-110 text-lb-bg font-semibold text-sm px-4 py-2.5 rounded-lg transition-all shadow-sm self-start sm:self-auto cursor-pointer"
          >
            + Nuevo Libro
          </button>
        )}
      </div>

      {/* Renderizado condicional del Formulario POST */}
      {mostrarFormulario && (
        <CrearLibroForm 
          onLibroCreado={manejarLibroCreado}
          onCancelar={() => setMostrarFormulario(false)}
        />
      )}

      <div className="mb-6 flex flex-col sm:flex-row sm:items-center justify-between gap-4">
        <input
          type="text"
          placeholder="🔍 Filtrar libros provenientes de PostgreSQL..."
          value={busqueda}
          onChange={(e) => setBusqueda(e.target.value)}
          className="w-full sm:max-w-md bg-lb-sidebar border border-lb-line rounded-lg px-4 py-2.5 text-sm text-white placeholder:text-lb-muted focus:outline-none focus:border-lb-green transition-colors"
        />
        <span className="text-xs text-lb-muted">
          Mostrando <strong className="text-white">{librosFiltrados.length}</strong> de {libros.length} registros en BD
        </span>
      </div>

      {cargando && (
        <div className="bg-lb-surface/50 border border-lb-line rounded-xl p-12 text-center">
          <p className="text-lb-green font-semibold animate-pulse text-lg">
            ⏳ Consultando endpoint GET /api/books en .NET...
          </p>
        </div>
      )}

      {!cargando && errorRed && (
        <div className="bg-red-950/40 border border-red-500/40 rounded-xl p-6 text-center">
          <p className="text-red-400 font-bold text-base mb-1">🚨 Error de Comunicación Full-Stack</p>
          <p className="text-lb-text text-sm">{errorRed}</p>
        </div>
      )}

      {!cargando && !errorRed && (
        <section className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-5">
          {librosFiltrados.map((libro) => (
            <LibroCard 
              key={libro.id} 
              id={libro.id} 
              title={libro.title} 
            />
          ))}
        </section>
      )}
    </div>
  )
}