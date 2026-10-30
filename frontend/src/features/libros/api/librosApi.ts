// src/features/libros/api/librosApi.ts
import type { Libro } from '../types/libro.types'

// Función asíncrona que retorna una Promesa con el arreglo de libros
export const obtenerLibrosAsync = (): Promise<Libro[]> => {
    return new Promise((resolve) => {
        setTimeout(() => {
            const datosCatalogo: Libro[] = [
                { id: 1, titulo: 'Cien años de soledad' },
                { id: 2, titulo: 'El Principito' },
                { id: 3, titulo: 'Don Quijote de la Mancha' },
                { id: 4, titulo: 'Clean Architecture' },
                { id: 5, titulo: 'Rayuela' },
                { id: 6, titulo: 'Pedro Páramo' },
            ]
            resolve(datosCatalogo)
        }, 1000)
    })
} 