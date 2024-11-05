import { ProductDto } from "./product-dto";

export interface PagedResults {
  pageNumber: number,
  pageSize: number,
  totalNumberOfPages: number,
  totalNumberOfRecords: number,
  resuls: ProductDto[]
}
