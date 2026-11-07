// frontend/src/features/libros/components/CrearLibroForm.tsx
import { useState } from 'react'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import type { Libro } from '../types/libro.types'
import { crearLibroAsync, extraerMensajeErrorBackend } from '../api/librosApi'
import { crearLibroSchema, type CrearLibroSchemaType } from '../schemas/libro.schema'

interface CrearLibroFormProps {
    onLibroCreado: (nuevoLibro: Libro) => void;
    onCancelar: () => void;
}

export function CrearLibroForm({ onLibroCreado, onCancelar }: CrearLibroFormProps) {
    // Estado para almacenar el mensaje exacto que devuelva .NET si rechaza la petición
    const [errorServidor, setErrorServidor] = useState<string | null>(null)

    // --- CONEXIÓN DEL ARTEFACTO DE VALIDACIÓN SINTÁCTICA EN TIEMPO REAL ---
    const {
        register,
        handleSubmit,
        reset,
        formState: { errors, isValid, isSubmitting },
    } = useForm<CrearLibroSchemaType>({
        resolver: zodResolver(crearLibroSchema),
        mode: 'onChange', // Evalúa cada tecla mientras el usuario escribe
        defaultValues: {
            title: '',
        },
    })

    // Solo se ejecuta cuando el Artefacto Zod garantiza que el dato está 100% limpio
    const procesarEnvioLimpio = async (datosValidados: CrearLibroSchemaType) => {
        try {
            setErrorServidor(null)

            const libroCreadoEnBd = await crearLibroAsync(datosValidados)

            reset()
            onLibroCreado(libroCreadoEnBd)
        } catch (error) {
            // Si .NET rechaza la operación (ej. Regla adicional en FluentValidation o Duplicado en BD),
            // extraemos y mostramos el mensaje exacto enviado por el Backend
            const mensajeExactoDeNet = extraerMensajeErrorBackend(error)
            console.error('🚨 Rechazo reportado por el Backend .NET:', error)
            setErrorServidor(mensajeExactoDeNet)
        }
    }

    return (
        <form 
            onSubmit={handleSubmit(procesarEnvioLimpio)} 
            className="bg-lb-surface border border-lb-green/40 rounded-xl p-5 mb-8 shadow-lg"
        >
            <div className="flex items-center justify-between mb-4">
                <h2 className="text-base font-bold text-white flex items-center gap-2">
                    <span className="text-lb-green">✨</span> Registrar Nuevo Libro en el Catálogo
                </h2>
                <span className="text-xs font-mono text-lb-green bg-lb-green/10 px-2.5 py-1 rounded border border-lb-green/30">
                    🛡️ Esquema Zod + FluentValidation .NET
                </span>
            </div>

            <div className="flex flex-col sm:flex-row gap-3">
                <div className="flex-1">
                    <input
                        type="text"
                        placeholder="Ej. Cien años de soledad..."
                        disabled={isSubmitting}
                        autoFocus
                        {...register('title', {
                            onChange: () => {
                                if (errorServidor) setErrorServidor(null)
                            },
                        })}
                        className={`w-full bg-lb-sidebar border rounded-lg px-4 py-2.5 text-sm text-white placeholder:text-lb-muted focus:outline-none transition-colors ${
                            errors.title 
                                ? 'border-lb-orange focus:border-lb-orange' 
                                : errorServidor
                                ? 'border-red-500 focus:border-red-400'
                                : 'border-lb-line focus:border-lb-green'
                        }`}
                    />
                </div>

                <div className="flex items-center gap-2 self-start">
                    <button
                        type="submit"
                        disabled={!isValid || isSubmitting}
                        className="bg-lb-green hover:brightness-110 disabled:bg-lb-surface disabled:text-lb-muted disabled:border disabled:border-lb-line text-lb-bg font-semibold text-sm px-5 py-2.5 rounded-lg transition-all cursor-pointer disabled:cursor-not-allowed"
                    >
                        {isSubmitting ? '⏳ Guardando en BD...' : '💾 Guardar en PostgreSQL'}
                    </button>

                    <button
                        type="button"
                        onClick={onCancelar}
                        disabled={isSubmitting}
                        className="bg-lb-line hover:brightness-125 text-lb-text font-medium text-sm px-4 py-2.5 rounded-lg transition-all cursor-pointer"
                    >
                        Cancelar
                    </button>
                </div>
            </div>

            {/* 1. Retroalimentación Sintáctica Instantánea (Zod MIENTRAS ESCRIBE) */}
            {errors.title && (
                <p className="text-lb-orange text-xs mt-2.5 font-medium flex items-center gap-1.5">
                    <span>⚡</span> <strong>Validación en vivo:</strong> {errors.title.message}
                </p>
            )}

            {/* 2. Retroalimentación del Servidor (Mensaje EXACTO devuelto por .NET FluentValidation) */}
            {errorServidor && (
                <div className="mt-3 bg-red-950/60 border border-red-500/40 rounded-lg p-3 text-xs text-red-300 flex items-center gap-2">
                    <span className="text-base">🛡️</span>
                    <span><strong>Respuesta del Backend (.NET):</strong> {errorServidor}</span>
                </div>
            )}
        </form>
    )
} 