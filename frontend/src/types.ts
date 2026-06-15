export interface Taxpayer {
  rncCedula: string;
  nombre: string;
  tipo: string;
  estatus: string;
}

export interface TaxReceipt {
  rncCedula: string;
  ncf: string;
  monto: number;
  itbis18: number;
}

export interface TaxpayerDetails extends Taxpayer {
  totalItbis: number;
  receipts: TaxReceipt[];
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}
