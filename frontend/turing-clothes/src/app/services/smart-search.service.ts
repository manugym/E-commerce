import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Result } from '../models/result';

@Injectable({
  providedIn: 'root'
})
export class SmartSearchService {

  constructor(private api: ApiService) { }

  async search(query: string): Promise<Result<string[]>> {
    return this.api.get<string[]>(`smartSearch?query=${query}`);
  }
}
