import { Component } from '@angular/core';
import { HeaderComponent } from '../header/header.component';
import { SmartSearchComponent } from '../smart-search/smart-search.component';

@Component({
  selector: 'app-catalogo',
  standalone: true,
  imports: [HeaderComponent, SmartSearchComponent],
  templateUrl: './catalogo.component.html',
  styleUrl: './catalogo.component.css'
})
export class CatalogoComponent {

}
