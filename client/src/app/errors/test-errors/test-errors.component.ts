import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.css']
})
export class TestErrorsComponent implements OnInit {
  baseUrl = 'https://localhost:5001/api/';
  validationErrors: string[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  getError(descriptor: string) {
    this.http.get(this.baseUrl + 'buggy/' + descriptor).subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
    });
  }

  get404Error() {
    this.getError('not-found');
  }

  get500Error() {
    this.getError('server-error');
  }

  get400Error() {
    this.getError('bad-request');
  }

  get401Error() {
    this.getError('auth');
  }

  get400ValidationError() {
    this.http.post(this.baseUrl + 'account/register', {}).subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
      this.validationErrors = error;
    });
  }

}
