import axios from 'axios';
import type { Taxpayer, TaxpayerDetails, TaxReceipt } from '../types';

const API_BASE_URL = 'http://localhost:5000/api';

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

export const getTaxpayers = async (): Promise<Taxpayer[]> => {
  const response = await apiClient.get<Taxpayer[]>('/taxpayers');
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
