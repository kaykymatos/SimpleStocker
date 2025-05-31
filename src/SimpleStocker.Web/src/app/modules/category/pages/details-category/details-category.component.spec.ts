import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailsCategoryComponent } from './details-category.component';

describe('DetailsCategoryComponent', () => {
  let component: DetailsCategoryComponent;
  let fixture: ComponentFixture<DetailsCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DetailsCategoryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetailsCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
