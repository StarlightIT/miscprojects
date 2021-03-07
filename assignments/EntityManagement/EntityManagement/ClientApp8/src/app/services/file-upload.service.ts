
import { Injectable } from '@angular/core';
import { Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable} from 'rxjs'; 
@Injectable({ 
  providedIn: 'root'
}) 
export class FileUploadService { 
    
  // API url 
  baseApiUrl = "https://file.io"
    
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 
      console.log(baseUrl);
      this.baseApiUrl = baseUrl + 'FileUpload/CsvPost';
  } 
  
  // Returns an observable 
  upload(file):Observable<any> { 
  
      // Create form data 
      const formData = new FormData();  
        
      // Store form name as "file" with file data 
      formData.append("CsvFile", file, file.name); 
        
      // Make http post request over api with formData as req 
      return this.http.post(this.baseApiUrl, formData);
  } 
} 
