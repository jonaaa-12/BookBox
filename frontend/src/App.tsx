// src/App.tsx
import { AppLayout } from './layouts/AppLayout'
import { CatalogoLibrosView } from './features/libros/components/CatalogoLibrosView'

function App() {
  return (
    <AppLayout>
      <CatalogoLibrosView />
    </AppLayout>
  )
}

export default App 