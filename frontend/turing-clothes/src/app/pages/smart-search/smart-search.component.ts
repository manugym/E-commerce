import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SmartSearchService } from '../../services/smart-search.service';

@Component({
  selector: 'app-smart-search',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './smart-search.component.html',
  styleUrl: './smart-search.component.css'
})
export class SmartSearchComponent implements OnInit {

  query: string = '';
  items: string[] = [];

  constructor(private smartSearchService: SmartSearchService) {}
  ngOnInit(): void {
    this.search();
  }

  async search() {
    const result = await this.smartSearchService.search(this.query);
    
    if (result.success) {
      this.items = result.data
    }
  }

}
