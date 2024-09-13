import swal from 'sweetalert2';
import { Component, OnInit } from '@angular/core';
import { LibeyUser } from 'src/app/entities/libeyuser';
import { LibeyUserService } from 'src/app/core/service/libeyuser/libeyuser.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
@Component({
  selector: 'app-usermaintenance',
  templateUrl: './usermaintenance.component.html',
  styleUrls: ['./usermaintenance.component.css']
})
export class UsermaintenanceComponent implements OnInit {
  
  user: LibeyUser = {
    documentNumber: '',
    documentTypeId: 0,
    name: '',
    fathersLastName: '',
    mothersLastName: '',
    address: '',
    regionCode: '',
    provinceCode: '',
    ubigeoCode: '',
    phone: '',
    email: '',
    password: '',
    active: true
  };
  
  isEditMode: boolean = false;

  documentTypes = [
    { id: 1, description: 'Documento Nacional de Identidad' },
    { id: 2, description: 'Registro Único de Contribuyente' }
  ];

  departments = [
    { id: '01', name: 'Lima' },
    { id: '02', name: 'Arequipa' }
  ];

  provinces = [
    { id: '01', name: 'Lima' },
    { id: '02', name: 'Arequipa' }
  ];

  districts = [
    { id: '01', name: 'Villa el Salvador' },
    { id: '02', name: 'Arequipa' }
  ];

  

  constructor(  private userService: LibeyUserService,
    private route: ActivatedRoute,
    private router: Router) { }
  ngOnInit(): void {

    const navigation = this.router.getCurrentNavigation();
    const state = navigation?.extras.state as { user?: LibeyUser };

    if (state && state.user) {
      this.user = state.user;
      this.isEditMode = true;
    }
  }
  
  submitForm(userForm: NgForm): void {
    if (userForm.valid) {
      
      const selectedDocumentType = this.documentTypes.find(type => type.id === this.user.documentTypeId);
      if (selectedDocumentType) {
        this.user.documentTypeId = selectedDocumentType.id;
      }
      this.user.ubigeoCode = `${this.user.regionCode}${this.user.provinceCode}${this.user.ubigeoCode}`;

     

      if (this.isEditMode) {
        this.userService.Update(this.user.documentNumber,this.user).subscribe(
          () => {
            this.router.navigate(['/user-list']);
          },
          (error) => {
            console.error('Error updating user', error);
          }
        );
      } else {

        this.userService.Create(this.user).subscribe(
          () => {
            this.router.navigate(['/user-list']);
          },
          (error) => {
            console.error('Error adding user', error);
          }
        );
      }
    }
  }

  clearForm(userForm: NgForm): void {
    userForm.resetForm();
    this.user = {
      documentNumber: '',
      documentTypeId: 0,
      name: '',
      fathersLastName: '',
      mothersLastName: '',
      address: '',
      regionCode: '',
      provinceCode: '',
      ubigeoCode: '',
      phone: '',
      email: '',
      password: '',
      active: true
    };
    this.isEditMode = false;
  }

  goBack(): void {
    this.router.navigate(['/user-list']);
  }
}