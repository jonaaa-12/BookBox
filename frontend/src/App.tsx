// src/App.tsx
import './App.css'
import { LibroCard } from './components/LibroCard'

function App() {
  return (
    <main>
      <h1>Catálogo de Libros</h1>
      <p>Explora los libros disponibles en nuestra base de datos.</p>
      
      {/* Reutilización del componente cumpliendo estrictamente el contrato (Id, Titulo) */}
      <section className="books-container">
        
        <LibroCard id={1} titulo="Cien años de soledad" />
        <LibroCard id={2} titulo="El Principito" />
        <LibroCard id={3} titulo="Don Quijote de la Mancha" />

      </section>
    </main>
  )
}

export default App 