import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getTaxpayers } from '../api/client';
import type { Taxpayer } from '../types';
import { Users } from 'lucide-react';

const TaxpayerList: React.FC = () => {
  const [taxpayers, setTaxpayers] = useState<Taxpayer[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchTaxpayers = async () => {
      try {
        const data = await getTaxpayers();
        setTaxpayers(data);
      } catch (err) {
        console.error(err);
        setError('Error al cargar la lista de contribuyentes.');
      } finally {
        setLoading(false);
      }
    };

    fetchTaxpayers();
  }, []);

  if (loading) return <div className="loading">Cargando contribuyentes...</div>;
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
              {taxpayers.map((taxpayer) => (
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
              ))}
              {taxpayers.length === 0 && (
                <tr>
                  <td colSpan={4} style={{ textAlign: 'center', padding: '30px' }}>
                    No se encontraron contribuyentes.
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
};

export default TaxpayerList;
