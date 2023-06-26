import { writable, type Writable } from 'svelte/store';

let errorState: Writable<{ state: boolean, message: string }> = writable({ state: false, message: '' });

export default errorState;