import axios from 'axios';
import type { Taxpayer, TaxpayerDetails, TaxReceipt, PagedResult } from '../types';

const API_BASE_URL = 'http://localhost:5000/api';

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

export const getTaxpayers = async (pageNumber: number = 1, pageSize: number = 10): Promise<PagedResult<Taxpayer>> => {
  const response = await apiClient.get<PagedResult<Taxpayer>>(`/taxpayers?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  return response.data;
};

export const getTaxpayerDetails = async (rncCedula: string): Promise<TaxpayerDetails> => {
  const response = await apiClient.get<TaxpayerDetails>(`/taxpayers/${rncCedula}`);
  return response.data;
};

export const getAllReceipts = async (): Promise<TaxReceipt[]> => {
  const response = await apiClient.get<TaxReceipt[]>('/taxpayers/receipts');
  return response.data;
};
