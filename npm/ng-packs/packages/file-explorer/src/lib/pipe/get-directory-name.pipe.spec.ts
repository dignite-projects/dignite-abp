import { GetDirectoryNamePipe } from './get-directory-name.pipe';

describe('GetDirectoryNamePipe', () => {
  it('create an instance', () => {
    const pipe = new GetDirectoryNamePipe();
    expect(pipe).toBeTruthy();
  });
});
