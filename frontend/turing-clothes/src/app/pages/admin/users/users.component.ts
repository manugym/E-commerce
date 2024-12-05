import { Component, OnInit } from '@angular/core';
import { SidebarComponent } from "../../../shared/sidebar/sidebar.component";
import { AdminService } from '../../../services/admin.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [SidebarComponent, RouterLink],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent implements OnInit {
  users: any[] = [];
  
  constructor(private adminService: AdminService) {}
  
  ngOnInit(): void {
    this.getUsers();
  }
  async getUsers(): Promise<void> {
    const result = await this.adminService.getUsers();
    if (result.success) {
      this.users = result.data;
    }
  }






  updateRol() {
  throw new Error('Method not implemented.');
  }
}
