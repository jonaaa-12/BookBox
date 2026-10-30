// src/layouts/AppLayout.tsx
import type { ReactNode } from 'react'

interface AppLayoutProps {
  children: ReactNode; // Representa la vista que se inyectará en el área de trabajo central
}

export function AppLayout({ children }: AppLayoutProps) {
  return (
    <div className="min-h-screen bg-lb-bg text-lb-text flex flex-col md:flex-row">
      
      {/* 1. COLUMNA IZQUIERDA: MENÚ CORPORATIVO (SIDEBAR) */}
      <aside className="hidden md:flex md:w-64 md:flex-col bg-lb-sidebar border-r border-lb-line justify-between p-5 shrink-0">
        <div>
          <div className="flex items-center gap-3 mb-8 px-2">
            <div className="w-8 h-8 rounded-lg bg-lb-green flex items-center justify-center font-bold text-lb-bg shadow">
              BB
            </div>
            <span className="text-xl font-bold tracking-tight text-white">BookBox</span>
          </div>

          <nav className="space-y-2">
            <a href="#" className="flex items-center gap-3 px-3 py-2.5 bg-lb-green/10 text-lb-green rounded-lg font-medium text-sm border border-lb-green/20">
              <span>📚</span> Catálogo de Libros
            </a>
            <a href="#" className="flex items-center gap-3 px-3 py-2.5 text-lb-muted rounded-lg font-medium text-sm cursor-not-allowed">
              <span>📖</span> Mi Biblioteca (Unidad 3)
            </a>
            <a href="#" className="flex items-center gap-3 px-3 py-2.5 text-lb-muted rounded-lg font-medium text-sm cursor-not-allowed">
              <span>📊</span> Mis Lecturas (Unidad 3)
            </a>
          </nav>
        </div>

        <div className="p-3.5 bg-lb-bg rounded-xl border border-lb-line">
          <p className="text-xs text-lb-muted mb-1">Estado de Seguridad</p>
          <p className="text-sm font-semibold text-lb-orange">👤 Modo Invitado</p>
        </div>
      </aside>

      {/* 2. CABECERA MÓVIL */}
      <header className="md:hidden bg-lb-sidebar border-b border-lb-line p-4 flex items-center justify-between">
        <div className="flex items-center gap-2.5">
          <div className="w-7 h-7 rounded-md bg-lb-green flex items-center justify-center font-bold text-sm text-lb-bg">
            BB
          </div>
          <span className="font-bold text-lg text-white">BookBox</span>
        </div>
        <span className="text-xs bg-lb-orange/10 text-lb-orange px-2.5 py-1 rounded-md border border-lb-orange/20 font-medium">
          👤 Invitado
        </span>
      </header>

      {/* 3. ÁREA CENTRAL DE CONTENIDO (Aquí se inyecta cualquier módulo hijo) */}
      <main className="flex-1 p-6 md:p-10 overflow-y-auto">
        {children}
      </main>
    </div>
  )
} 