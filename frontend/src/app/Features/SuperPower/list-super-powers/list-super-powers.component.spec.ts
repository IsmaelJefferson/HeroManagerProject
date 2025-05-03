import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListSuperPowersComponent } from './list-super-powers.component';

describe('ListSuperPowersComponent', () => {
  let component: ListSuperPowersComponent;
  let fixture: ComponentFixture<ListSuperPowersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListSuperPowersComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListSuperPowersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
