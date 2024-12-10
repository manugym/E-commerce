import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { EditDto } from '../../../models/edit-dto';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-edit',
  standalone: true,
  imports: [],
  templateUrl: './edit.component.html',
  styleUrl: './edit.component.css'
})
export class EditComponent implements OnInit{
saveChanges() {
throw new Error('Method not implemented.');
}

  userInfo: EditDto;

  editName: boolean = false;
  editSurname: boolean = false;
  editEmail: boolean = false;
  editAddress: boolean = false;

  constructor(private authService: AuthService, private userService: UserService){

  }
  ngOnInit(): void {
    this.getUser()
  }

  async getUser(): Promise<void>{
    const result = await this.userService.getEditUser()
    this.userInfo = result.data;
  }
}
