import { Component, Input } from '@angular/core';
import { NPVRangeResponse, NPVResponse } from '../models/npv-response.model';

@Component({
  selector: 'app-npv-cash-flow-result',
  templateUrl: './npv-cash-flow-result.component.html',
  styleUrl: './npv-cash-flow-result.component.css',
})
export class NpvCashFlowResultComponent {
  @Input() npvWithCashFlowResult: NPVResponse | null = null;
  @Input() npvRangeResult: NPVRangeResponse[] | null = null;
}
