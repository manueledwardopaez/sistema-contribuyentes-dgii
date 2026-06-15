import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getTaxpayerDetails } from '../api/client';
import type { TaxpayerDetails } from '../types';
import { ArrowLeft, Receipt } from 'lucide-react';

const TaxpayerDetail: React.FC = () => {
  const { rncCedula } = useParams<{ rncCedula: string }>();
  const navigate = useNavigate();
  const [details, setDetails] = useState<TaxpayerDetails | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchDetails = async () => {
      try {
        if (rncCedula) {
          const data = await getTaxpayerDetails(rncCedula);
          setDetails(data);
        }
      } catch (err: any) {
        console.error(err);
        if (err.response?.status === 404) {
          setError('Contribuyente no encontrado.');
        } else {
          setError('Error al cargar los detalles del contribuyente.');
        }
      } finally {
        setLoading(false);
      }
    };

    fetchDetails();
  }, [rncCedula]);

  if (loading) return <div className="loading">Cargando detalles...</div>;
  if (error) return (
    <div className="fade-in app-container">
      <button className="btn-back" onClick={() => navigate('/')}>
        <ArrowLeft size={18} /> Volver
      </button>
      <div className="error">{error}</div>
    </div>
  );
  if (!details) return null;

  return (
    <div className="fade-in">
      <button className="btn-back" onClick={() => navigate('/')}>
        <ArrowLeft size={18} /> Volver al listado
      </button>

      <div className="header" style={{ textAlign: 'left' }}>
        <h1 style={{ fontSize: '2rem' }}>{details.nombre}</h1>
        <p style={{ color: 'var(--text-secondary)', fontSize: '1.1rem' }}>
          RNC/Cédula: <strong>{details.rncCedula}</strong> | Tipo: <strong>{details.tipo}</strong>
        </p>
      </div>

      <div className="glass-panel" style={{ marginBottom: '30px' }}>
        <h2 style={{ display: 'flex', alignItems: 'center', gap: '10px', marginTop: 0, color: 'var(--text-secondary)' }}>
          <Receipt size={24} /> Comprobantes Fiscales
        </h2>
        
        <div className="table-container">
          <table>
            <thead>
              <tr>
                <th>NCF</th>
                <th style={{ textAlign: 'right' }}>Monto</th>
                <th style={{ textAlign: 'right' }}>ITBIS (18%)</th>
              </tr>
            </thead>
            <tbody>
              {details.receipts.map((receipt) => (
                <tr key={receipt.ncf} style={{ cursor: 'default' }}>
                  <td style={{ fontFamily: 'monospace', fontSize: '1.1rem' }}>{receipt.ncf}</td>
                  <td style={{ textAlign: 'right' }}>
                    ${receipt.monto.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
                  </td>
                  <td style={{ textAlign: 'right', color: 'var(--primary-color)', fontWeight: 'bold' }}>
                    ${receipt.itbis18.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
                  </td>
                </tr>
              ))}
              {details.receipts.length === 0 && (
                <tr>
                  <td colSpan={3} style={{ textAlign: 'center', padding: '30px' }}>
                    No hay comprobantes reportados.
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      </div>

      <div className="summary-card fade-in" style={{ animationDelay: '0.2s' }}>
        <div>
          <h3>Total ITBIS</h3>
          <p style={{ margin: 0, color: 'var(--text-secondary)', marginTop: '8px' }}>
            Suma del ITBIS de todos los comprobantes reportados.
          </p>
        </div>
        <div className="summary-amount">
          ${details.totalItbis.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
        </div>
      </div>
    </div>
  );
};

export default TaxpayerDetail;
