import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators, } from '@angular/forms';

@Component({
  selector: 'app-fields',
  templateUrl: './fields.component.html',
  styleUrl: './fields.component.scss'
})
export class FieldsComponent implements OnInit {
nestedForm: FormGroup;
  errors: string[] = [];

  constructor(private fb: FormBuilder) {}

  ngOnInit() {
    this.nestedForm = this.fb.group({
      group1: this.fb.group({
        control1: ['', Validators.required],
        control2: ['', Validators.required],
        subGroup1: this.fb.group({
          subControl1: ['', Validators.required],
          array1: this.fb.array([
            this.fb.group({
              itemControl1: ['', Validators.required],
              subSubGroup1: this.fb.group({
                subSubControl1: ['', Validators.required],
                subArray1: this.fb.array([
                  this.fb.group({
                    subControlItem1: ['', Validators.required],
                  }),
                  this.fb.group({
                    subControlItem2: ['', Validators.required],
                  }),
                ]),
              }),
            }),
          ]),
        }),
        array2: this.fb.array([
          this.fb.group({
            itemControl2: ['', Validators.required],
            subGroup2: this.fb.group({
              subControl2: ['', Validators.required],
            }),
            subArray2: this.fb.array([
              this.fb.group({
                subControlItem2: ['', Validators.required],
              }),
              this.fb.group({
                subControlItem3: ['', Validators.required],
              })
            ]),
          }),
        ]),
      }),
    });
  }

  validateForm() {
    this.errors = [];
    this.validateControls(this.nestedForm);
  }

  private validateControls(control: any, path: string[] = []) {
    if (control instanceof FormControl) {
      if (control.invalid) {
        this.errors.push(`${path.join('-')} is invalid`);
      }
    } else if (control instanceof FormGroup) {
      Object.keys(control.controls).forEach(key => {
        this.validateControls(control.get(key), [...path, key]);
      });
    } else if (control instanceof FormArray) {
      control.controls.forEach((ctrl, index) => {
        this.validateControls(ctrl, [...path, `${index}`]);
      });
    }
  }
}
