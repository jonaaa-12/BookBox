// frontend/src/features/libros/schemas/libro.schema.ts
import { z } from 'zod'

// Artefacto Declarativo de Validación Sintáctica (Espejo de FluentValidation en .NET)
export const crearLibroSchema = z.object({
    title: z
        .string()
        .trim() // Sanitiza espacios en blanco al inicio y al final
        .min(1, 'El título del libro no puede estar vacío.')
        .min(3, 'El título debe tener al menos 3 caracteres.')
        .max(200, 'El título no puede superar los 200 caracteres permitidos en PostgreSQL.')
        .regex(
            /^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ0-9\s.,:;()¿?¡!'"&-]+$/,
            'El título contiene caracteres especiales no permitidos.'
        ),
})

export type CrearLibroSchemaType = z.infer<typeof crearLibroSchema> 