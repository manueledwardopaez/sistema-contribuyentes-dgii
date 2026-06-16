import React, { useEffect, useState } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { getTaxpayers } from '../api/client';
import type { Taxpayer } from '../types';
import { Users, ChevronLeft, ChevronRight } from 'lucide-react';

const TaxpayerList: React.FC = () => {
  const [taxpayers, setTaxpayers] = useState<Taxpayer[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  // Pagination state
  const [searchParams, setSearchParams] = useSearchParams();
  const page = parseInt(searchParams.get('page') || '1', 10);
  const [totalPages, setTotalPages] = useState<number>(1);
  const pageSize = 5;

  const navigate = useNavigate();

  useEffect(() => {
    const fetchTaxpayers = async () => {
      setLoading(true);
      try {
        const data = await getTaxpayers(page, pageSize);
        setTaxpayers(data.items);
        setTotalPages(data.totalPages);
      } catch (err) {
        console.error(err);
        setError('Error al cargar la lista de contribuyentes.');
      } finally {
        setLoading(false);
      }
    };

    fetchTaxpayers();
  }, [page]);

  if (error) return <div className="error">{error}</div>;

  return (
    <div className="fade-in">
      <div className="header">
        <h1><Users className="inline-block mr-3 mb-1" size={36} /> Directorio de Contribuyentes</h1>
        <p style={{ color: 'var(--text-secondary)' }}>Seleccione un contribuyente para ver sus comprobantes y calcular el total de ITBIS.</p>
      </div>

      <div className="glass-panel">
        <div className="table-container">
          <table>
            <thead>
              <tr>
                <th>RNC / Cédula</th>
                <th>Nombre</th>
                <th>Tipo</th>
                <th>Estatus</th>
              </tr>
            </thead>
            <tbody>
              {loading ? (
                <tr>
                  <td colSpan={4} style={{ textAlign: 'center', padding: '30px' }}>
                    <div className="loading">Cargando contribuyentes...</div>
                  </td>
                </tr>
              ) : taxpayers.length === 0 ? (
                <tr>
                  <td colSpan={4} style={{ textAlign: 'center', padding: '30px' }}>
                    No se encontraron contribuyentes.
                  </td>
                </tr>
              ) : (
                taxpayers.map((taxpayer) => (
                  <tr
                    key={taxpayer.rncCedula}
                    onClick={() => navigate(`/taxpayer/${taxpayer.rncCedula}`)}
                  >
                    <td style={{ fontWeight: 600 }}>{taxpayer.rncCedula}</td>
                    <td>{taxpayer.nombre}</td>
                    <td>{taxpayer.tipo}</td>
                    <td>
                      <span className={`status-badge status-${taxpayer.estatus.toLowerCase()}`}>
                        {taxpayer.estatus}
                      </span>
                    </td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>

        {/* Pagination Controls */}
        {!loading && taxpayers.length > 0 && (
          <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', padding: '20px 20px 10px', borderTop: '1px solid var(--border-color)' }}>
            <button
              className="btn-back"
              style={{ margin: 0, padding: '8px 16px', border: '1px solid var(--border-color)' }}
              disabled={page <= 1}
              onClick={() => setSearchParams({ page: Math.max(1, page - 1).toString() })}
            >
              <ChevronLeft size={18} /> Anterior
            </button>
            <span style={{ color: 'var(--text-secondary)' }}>
              Página <strong>{page}</strong> de <strong>{totalPages || 1}</strong>
            </span>
            <button
              className="btn-back"
              style={{ margin: 0, padding: '8px 16px', border: '1px solid var(--border-color)', flexDirection: 'row-reverse' }}
              disabled={page >= totalPages}
              onClick={() => setSearchParams({ page: Math.min(totalPages, page + 1).toString() })}
            >
              <ChevronRight size={18} /> Siguiente
            </button>
          </div>
        )}
      </div>
    </div>
  );
};

export default TaxpayerList;
