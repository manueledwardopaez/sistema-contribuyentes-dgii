import { render, screen, waitFor } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { describe, it, expect, vi, beforeEach } from 'vitest';
import userEvent from '@testing-library/user-event';
import TaxpayerList from '../TaxpayerList';
import * as client from '../../api/client';
import type { Taxpayer, PagedResult } from '../../types';

// Mock the API client
vi.mock('../../api/client');

describe('TaxpayerList Component', () => {
  beforeEach(() => {
    vi.clearAllMocks();
  });

  it('renders loading state initially', () => {
    // Return an unresolved promise to simulate loading
    vi.mocked(client.getTaxpayers).mockImplementation(() => new Promise(() => {}));
    
    render(
      <MemoryRouter>
        <TaxpayerList />
      </MemoryRouter>
    );

    expect(screen.getByText(/cargando contribuyentes/i)).toBeInTheDocument();
  });

  it('renders taxpayer data successfully', async () => {
    const mockPagedResult: PagedResult<Taxpayer> = {
      items: [
        { rncCedula: '123456789', nombre: 'Juan Perez', tipo: 'FISICA', estatus: 'ACTIVO' },
        { rncCedula: '987654321', nombre: 'Empresa XYZ', tipo: 'JURIDICA', estatus: 'INACTIVO' }
      ],
      totalCount: 2,
      pageNumber: 1,
      pageSize: 10,
      totalPages: 1
    };

    vi.mocked(client.getTaxpayers).mockResolvedValue(mockPagedResult);

    render(
      <MemoryRouter>
        <TaxpayerList />
      </MemoryRouter>
    );

    // Wait for the data to load and remove the loading indicator
    await waitFor(() => {
      expect(screen.queryByText(/cargando contribuyentes/i)).not.toBeInTheDocument();
    });

    // Check if the taxpayers are rendered
    expect(screen.getByText('123456789')).toBeInTheDocument();
    expect(screen.getByText('Juan Perez')).toBeInTheDocument();
    expect(screen.getByText('987654321')).toBeInTheDocument();
    expect(screen.getByText('Empresa XYZ')).toBeInTheDocument();
    
    // Check pagination controls
    expect(screen.getByText(/Página/i)).toBeInTheDocument();
    const pageNumbers = screen.getAllByText('1');
    expect(pageNumbers.length).toBeGreaterThanOrEqual(2); // current page and total pages
  });

  it('renders error message when API fails', async () => {
    vi.mocked(client.getTaxpayers).mockRejectedValue(new Error('API Error'));

    render(
      <MemoryRouter>
        <TaxpayerList />
      </MemoryRouter>
    );

    await waitFor(() => {
      expect(screen.queryByText(/cargando contribuyentes/i)).not.toBeInTheDocument();
    });

    expect(screen.getByText(/error al cargar la lista de contribuyentes/i)).toBeInTheDocument();
  });

  it('renders empty state when no taxpayers are returned', async () => {
    const mockEmptyResult: PagedResult<Taxpayer> = {
      items: [],
      totalCount: 0,
      pageNumber: 1,
      pageSize: 10,
      totalPages: 0
    };
    
    vi.mocked(client.getTaxpayers).mockResolvedValue(mockEmptyResult);

    render(
      <MemoryRouter>
        <TaxpayerList />
      </MemoryRouter>
    );

    await waitFor(() => {
      expect(screen.getByText(/no se encontraron contribuyentes/i)).toBeInTheDocument();
    });
  });
  
  it('handles pagination next and previous buttons', async () => {
    const user = userEvent.setup();
    const mockPage1: PagedResult<Taxpayer> = {
      items: [{ rncCedula: '111', nombre: 'Page 1', tipo: 'FISICA', estatus: 'ACTIVO' }],
      totalCount: 20, pageNumber: 1, pageSize: 10, totalPages: 2
    };
    const mockPage2: PagedResult<Taxpayer> = {
      items: [{ rncCedula: '222', nombre: 'Page 2', tipo: 'FISICA', estatus: 'ACTIVO' }],
      totalCount: 20, pageNumber: 2, pageSize: 10, totalPages: 2
    };

    // First call returns page 1, subsequent calls return page 2
    vi.mocked(client.getTaxpayers)
      .mockResolvedValueOnce(mockPage1)
      .mockResolvedValueOnce(mockPage2);

    render(
      <MemoryRouter>
        <TaxpayerList />
      </MemoryRouter>
    );

    await waitFor(() => {
      expect(screen.getByText('Page 1')).toBeInTheDocument();
    });

    const nextBtn = screen.getByRole('button', { name: /siguiente/i });
    const prevBtn = screen.getByRole('button', { name: /anterior/i });

    // Prev should be disabled on page 1
    expect(prevBtn).toBeDisabled();
    expect(nextBtn).not.toBeDisabled();

    // Click Next
    await user.click(nextBtn);

    await waitFor(() => {
      expect(screen.getByText('Page 2')).toBeInTheDocument();
    });
    
    // Check it called API with page 2
    expect(client.getTaxpayers).toHaveBeenCalledWith(2, 10);
  });
});
