// utils/base64Utils.ts
export const decodeBase64 = (input: string): string => {
    // Kiểm tra xem chuỗi đầu vào có hợp lệ không
    if (typeof input !== 'string') {
      throw new Error('Input must be a string');
    }
  
    // Thực hiện giải mã base64
    try {
      const decodedString = atob(input);
      return decodedString;
    } catch (error) {
      throw new Error('Invalid Base64 input');
    }
  };