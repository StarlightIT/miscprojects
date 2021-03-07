import { Component } from '@angular/core';
import { FileUploadService } from '../services/file-upload.service'; 

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  file: File = null;

  title = 'Browser market shares at a specific website, 2014';
  type='PieChart';
  data = [
    ['Firefox', 45.0],
    ['IE', 26.8],
    ['Chrome', 12.8],
    ['Safari', 8.5],
    ['Opera', 6.2],
    ['Others', 0.7] 
  ];
  columnNames = ['Browser', 'Percentage'];
  options = {
    colors: ['#e0440e', '#e6693e', '#ec8f6e', '#f3b49f', '#f6c7b6'], is3D: true
  };

  constructor(private fileUploadService: FileUploadService) { }

  onChange(event) { 
    this.file = event.target.files[0]; 
  }
  
  onUpload() { 
    console.log(this.file); 
    this.fileUploadService.upload(this.file).subscribe( 
        (event: any) => { 
            if (typeof (event) === 'object') { 
              console.log('File Uploaded');
            } 
        } 
    ); 
  } 

}
