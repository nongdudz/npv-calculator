import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NPVRequest } from '../models/npv-request.model';
import { NPVRangeResponse, NPVResponse } from '../models/npv-response.model';

@Injectable({
  providedIn: 'root',
})
export class NpvService {
  constructor(private http: HttpClient) {}

  calculateNPVWithCashFlowStream(
    npvRequest: NPVRequest
  ): Observable<NPVResponse> {
    return this.http.post<NPVResponse>(`/api/calculator/npv`, npvRequest);
  }

  calculateNPVRangeWithCashFlowStream(
    npvRequest: NPVRequest
  ): Observable<NPVRangeResponse[]> {
    return this.http.post<NPVRangeResponse[]>(
      `api/calculator/npv-range`,
      npvRequest
    );
  }
}
