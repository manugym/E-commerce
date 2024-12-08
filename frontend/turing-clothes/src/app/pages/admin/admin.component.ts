import { Component } from '@angular/core';
import { UsersComponent } from "./users/users.component";
import { SidebarComponent } from "../../shared/sidebar/sidebar.component";
@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [UsersComponent, SidebarComponent],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent {
}
