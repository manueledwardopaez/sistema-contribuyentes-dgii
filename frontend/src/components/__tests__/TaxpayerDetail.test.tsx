import { render, screen, waitFor } from '@testing-library/react';
import { MemoryRouter, Route, Routes } from 'react-router-dom';
import { describe, it, expect, vi, beforeEach } from 'vitest';
import TaxpayerDetail from '../TaxpayerDetail';
import * as client from '../../api/client';
import type { TaxpayerDetails } from '../../types';

vi.mock('../../api/client');

describe('TaxpayerDetail Component', () => {
  beforeEach(() => {
    vi.clearAllMocks();
  });

  const renderComponent = (rncCedula: string) => {
    render(
      <MemoryRouter initialEntries={[`/taxpayer/${rncCedula}`]}>
        <Routes>
          <Route path="/taxpayer/:rncCedula" element={<TaxpayerDetail />} />
        </Routes>
      </MemoryRouter>
    );
  };

  it('renders loading state initially', () => {
    vi.mocked(client.getTaxpayerDetails).mockImplementation(() => new Promise(() => {}));
    
    renderComponent('123456789');

    expect(screen.getByText(/cargando detalles/i)).toBeInTheDocument();
  });

  it('renders taxpayer details and receipts successfully', async () => {
    const mockDetails: TaxpayerDetails = {
      rncCedula: '123456789',
      nombre: 'FARMACIA TU SALUD',
      tipo: 'PERSONA JURIDICA',
      estatus: 'activo',
      totalItbis: 450.50,
      receipts: [
        { rncCedula: '123456789', ncf: 'E310000000001', monto: 2502.77, itbis18: 450.50 }
      ]
    };

    vi.mocked(client.getTaxpayerDetails).mockResolvedValue(mockDetails);

    renderComponent('123456789');

    await waitFor(() => {
      expect(screen.queryByText(/cargando detalles/i)).not.toBeInTheDocument();
    });

    // Check header information
    expect(screen.getByText('FARMACIA TU SALUD')).toBeInTheDocument();
    expect(screen.getByText('123456789')).toBeInTheDocument();
    expect(screen.getByText('PERSONA JURIDICA')).toBeInTheDocument();

    // Check receipts table
    expect(screen.getByText('E310000000001')).toBeInTheDocument();
    
    // Total ITBIS is rendered formatting as currency
    const amounts = screen.getAllByText('$450.50');
    expect(amounts.length).toBeGreaterThan(0);
  });

  it('renders error message when taxpayer is not found (404)', async () => {
    const errorResponse = { response: { status: 404 } };
    vi.mocked(client.getTaxpayerDetails).mockRejectedValue(errorResponse);

    renderComponent('000000000');

    await waitFor(() => {
      expect(screen.getByText(/contribuyente no encontrado/i)).toBeInTheDocument();
    });
  });

  it('renders generic error message on network failure', async () => {
    vi.mocked(client.getTaxpayerDetails).mockRejectedValue(new Error('Network Error'));

    renderComponent('123456789');

    await waitFor(() => {
      expect(screen.getByText(/error al cargar los detalles del contribuyente/i)).toBeInTheDocument();
    });
  });
});
